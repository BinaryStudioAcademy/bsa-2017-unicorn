import { Component, OnInit, OnDestroy, ViewChild, NgZone } from '@angular/core';
import { DatePipe } from '@angular/common';

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';
import { CompanyTask } from '../../../models/book/book.model';
import { ShortTask } from '../../../models/dashboard/company-task';
import { Vendor } from '../../../models/company-page/vendor';
import { Work } from '../../../models/work.model';
import { ShortReview } from '../../../models/short-review';

import { DashboardService } from '../../../services/dashboard/dashboard.service';
import { DashMessagingService } from '../../../services/dashboard/dash-messaging.service';
import { NotificationService } from "../../../services/notifications/notification.service";
import { ReviewService } from '../../../services/review.service';

import {ToastsManager, Toast} from 'ng2-toastr';
import {ToastOptions} from 'ng2-toastr';
import {SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize, SuiActiveModal} from 'ng2-semantic-ui';

import { Subscription } from 'rxjs/Subscription';
import { MapModel } from "../../../models/map.model";
import { DashboardEventsService } from "../../../services/events/dashboard-events.service";

export interface IDeclineConfirm {
  id: number;
}

export interface IDeclinedReasonContext {
  reason: string;
}

export interface IReviewContext {
  id: number;
}

@Component({
  selector: 'app-dashboard-company-progress',
  templateUrl: './dashboard-company-progress.component.html',
  styleUrls: ['./dashboard-company-progress.component.sass']
})
export class DashboardCompanyProgressComponent implements OnInit, OnDestroy {

  @ViewChild('reviewTemplate')
  public reviewTemplate:ModalTemplate<IReviewContext, void, void>;

  @ViewChild('mapModal')
  public modalTemplate:ModalTemplate<IDeclineConfirm, void, void>;

  @ViewChild('reasonModal')
  public reasonModal:ModalTemplate<IDeclinedReasonContext, void, void>;

  @ViewChild('declineModal')
  public declineTemplate:ModalTemplate<IDeclineConfirm, void, void>

  @ViewChild('reassignModal')
  public reassignTemplate:ModalTemplate<void, void, void>

  currModal: SuiActiveModal<IDeclineConfirm, {}, void>;
  books: BookCard[];
  loads: {[bookId: number]: boolean} = {};
  sub: Subscription;
  map: MapModel;

  tasks: CompanyTask[];

  constructor(
    private zone: NgZone,
    private reviewService: ReviewService,
    private dashboardService: DashboardService,
    private dashMessaging: DashMessagingService,
    private notificationService: NotificationService,
    private toastr: ToastsManager,
    private modalService: SuiModalService,
    private datePipe: DatePipe,
    private dashboardEventsService: DashboardEventsService
  ) { }

  ngOnInit() {
    this.loadData();
    this.sub = this.dashMessaging.pendingEvent$.subscribe(() => {
      this.loadData();
    });
    this.notificationService.listen<any>("RefreshOrders", () => this.loadData());
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  loadData(): Promise<any> {
    this.dashboardService.getCompanyVendorsWithWorks().then(resp => {
      this.vendors = [];
      this.availableVendors = [];
      resp.forEach(x => {
        this.vendors.push(x);
        this.availableVendors.push(x);
      });
    });
    return this.dashboardService.getCompanyTasks().then(resp => {
      this.tasks = resp;
      console.log('ra', this.tasks);
      return this.dashboardService.getAcceptedBooks();
    }).then(resp => {
      this.books = resp.sort((b1, b2) => {
        let f = new Date(b1.Date).getTime();
        let s = new Date(b2.Date).getTime();
        return s - f;
      });
      console.log(this.books);
    });
  }

  createTasks(id: number) {
    let existingTasks = this.tasks.filter(t => t.ParentBookId === id);
    this.availableVendors = this.vendors.filter(v => existingTasks.findIndex(t => t.Vendor.Id === v.Id) === -1);
    this.taskFormOpen[id] = true;
    this.someFormOpened = true;
  }

  finish(id: number) {
    let book: BookCard = this.books.filter(b => b.Id == id)[0];
    book.Status = BookStatus.Finished;
    this.loads[book.Id] = true;
    this.dashboardService.update(book).then(resp => {
      this.dashboardEventsService.changeStatusToFinished();
      this.books.splice(this.books.findIndex(b => b.Id === id), 1);
      this.loads[book.Id] = false;
      this.dashMessaging.changeProgress();
      this.toastr.success('Finished task');
    })
    .catch(err => {
      this.loads[book.Id] = false;      
      this.toastr.error('Cannot finish task');
    });
  }

  getEndDate(book: BookCard): string {
    let date = this.datePipe.transform(book.Date, 'dd/MM/yyyy');
    let endDate = this.datePipe.transform(book.EndDate, 'dd/MM/yyyy');
    if (date == endDate) {
      return '';
    }
    return ` - ${endDate}`;
  }

  openMap(id:number)
  { 
    this.map = {
      center: {lat: this.books.filter(b => b.Id == id)[0].Location.Latitude, lng: this.books[0].Location.Longitude},
      zoom: 18,    
      title: this.books.filter(b => b.Id == id)[0].Customer,
      label: this.books.filter(b => b.Id == id)[0].Customer,
      markerPos: {lat: this.books.filter(b => b.Id == id)[0].Location.Latitude, lng: this.books.filter(b => b.Id == id)[0].Location.Longitude}    
    };  
   const config = new TemplateModalConfig<IDeclineConfirm, void, void>(this.modalTemplate);
   config.isInverted = true;
   config.size = ModalSize.Tiny;
   this.currModal = this.modalService.open(config);
  }

  calcAllTasks(id: number): number {
    return this.tasks.filter(t => t.ParentBookId === id).length;
  }

  calcFinishedTasks(id: number): number {
    return this.tasks.filter(t => t.ParentBookId === id && (t.Status === BookStatus.Finished || t.Status === BookStatus.Confirmed)).length;
  }

  calcConfirmedTasks(id: number): number {
    return this.tasks.filter(t => t.ParentBookId === id && t.Status === BookStatus.Confirmed).length;
  }

  

  //vendor assign

  review: ShortReview = {
    BookId: 0,
    Grade: 0,
    PerformerId: 0,
    PerformerType: '',
    Text: ''
  };
  gradeSelected: boolean;
  reviewLoader: boolean;

  assignLoader: boolean;
  editMode: boolean;
  loader: boolean;
  reassignLoader: boolean;

  shortTasks: ShortTask[] = [];
  taskListOpened: {[bookId: number]: boolean} = {};

  dloads: {[bookId: number]: boolean} = {};

  vendors: Vendor[] = [];
  availableVendors: Vendor[] = [];
  selectedVendor: Vendor;

  works: Work[] = [];
  selectedWork: Work;
  selectedTask: ShortTask;
  description: string;

  reason: string;

  assignedBooks: {[bookId: number]: boolean} = {};

  changeVendor() {
    this.selectedWork = null;
    this.works = this.selectedVendor.Works;
  }

  taskFormOpened: boolean;
  someFormOpened: boolean;
  taskFormOpen: {[bookId: number]: boolean} = {};

  currentTask: ShortTask = {
    Id: null,
    BookId: null,
    DeclinedReason: null,
    Description: null,
    WorkId: null,
    VendorId: null
  };

  reassignTask: ShortTask;

  newTask(bookId: number) {
    this.editMode = false;
    this.currentTask = {
      Id: null,
      BookId: bookId,
      DeclinedReason: null,
      Description: null,
      WorkId: null,
      VendorId: null
    };
    this.restoreAvailableVendorsFromBookIdShort(bookId);
    this.taskFormOpened = true;
    this.selectedWork = null;
    this.selectedVendor = null;
    
  }

  saveTask(bookId: number) {
    let oldVend = this.currentTask.VendorId;
    this.currentTask.WorkId = this.selectedWork.Id;
    this.currentTask.VendorId = this.selectedVendor.Id;
    if (this.editMode) {
      let oldTaskIndex = this.shortTasks.findIndex(t => t.VendorId === oldVend);
      this.shortTasks[oldTaskIndex].Description = this.currentTask.Description;
      this.shortTasks[oldTaskIndex].VendorId = this.selectedVendor.Id;
      this.shortTasks[oldTaskIndex].WorkId = this.selectedWork.Id;
    } else {
      this.availableVendors.splice(this.availableVendors.findIndex(v => v.Id === this.selectedVendor.Id), 1);
      this.shortTasks.push(this.currentTask);
    }
    this.taskFormOpened = false;
    this.editMode = false;
  }

  asignVendors(id: number) {
    console.log(this.tasks);
    this.assignLoader = true;
    this.dashboardService.createTasks(this.shortTasks).then(resp => {
      this.toastr.success("Tasks created");
      
      this.restoreAvailableVendors();
      this.loadData().then(() => {
        this.assignLoader = false;
        this.assignedBooks[id] = true;
        this.taskFormOpen[id] = false;
        this.taskFormOpened = false;
        this.someFormOpened = false;
        this.editMode = false;
        this.shortTasks = [];
      });
    }).catch(err => {
      this.toastr.error("Problems");
      this.assignLoader = false;
    });
  }

  cancelAssign(id: number) {
    this.shortTasks = [];
    this.restoreAvailableVendors();
    this.taskFormOpen[id] = false;
    this.taskFormOpened = false;
    this.someFormOpened = false;
    this.editMode = false;
  }

  restoreAvailableVendors() {
    this.availableVendors = [];
    this.vendors.forEach(v => this.availableVendors.push(v));
  }

  restoreAvailableVendorsFromBookId(id: number) {
    let existingTasks = this.tasks.filter(t => t.ParentBookId === id);
    this.availableVendors = this.vendors.filter(v => existingTasks.findIndex(t => t.Vendor.Id === v.Id) === -1);
  }

  restoreAvailableVendorsFromBookIdShort(id: number) {
    let existingShortTasks = this.shortTasks.filter(t => t.BookId === id);
    let existingTasks = this.tasks.filter(t => t.ParentBookId === id);
    this.availableVendors = this.vendors.filter(v => existingShortTasks.findIndex(t => t.VendorId === v.Id) === -1);
    this.availableVendors = this.availableVendors.filter(v => existingTasks.findIndex(t => t.Vendor.Id === v.Id) === -1);
  }

  getVendorIcon(task: ShortTask): string {
    debugger;
    let ven = this.vendors.filter(v => v.Id === task.VendorId)
    let ve = ven[0];
    return ve.Avatar;
  }

  editTask(task: ShortTask) {
    this.editMode = true;
    this.currentTask = {
      Id: null,
      BookId: task.BookId,
      DeclinedReason: null,
      Description: task.Description,
      WorkId: null,
      VendorId: task.VendorId
    };
    let vendor = this.vendors.filter(v => v.Id === task.VendorId)[0];
    if (this.availableVendors.findIndex(v => v.Id === vendor.Id) == -1) {
      this.availableVendors.push(vendor);
    }
    this.zone.run(() => {
      this.selectedVendor = vendor;
      this.works = this.selectedVendor.Works;
      this.selectedWork = this.works.filter(w => w.Id === task.WorkId)[0];
      this.taskFormOpened = true;
    });
  }

  deleteTask(task: ShortTask) {
    this.shortTasks.splice(this.shortTasks.findIndex(t => t.VendorId === task.VendorId), 1);
    this.restoreAvailableVendorsFromBookId(task.BookId);
    this.taskFormOpened = false;
    this.editMode = false;
  }

  getVendorAvatar(task: ShortTask): string {
    return this.vendors.filter(v => v.Id === task.VendorId)[0].Avatar;
  }

  getVendorFio(task: ShortTask): string {
    return this.vendors.filter(v => v.Id === task.VendorId)[0].FIO;
  }

  isVendorAssigned(id: number): boolean {
    return this.tasks.filter(t => t.ParentBookId === id).length > 0;
  }

  getTasksByBook(id: number): CompanyTask[] {
    return this.tasks.filter(t => t.ParentBookId === id);
  }

  getStatusName(status: BookStatus): string {
    switch (status) {
      case BookStatus.Pending: return 'Pending..';
      case BookStatus.Accepted: return 'In progress';
      case BookStatus.Declined: return 'Declined';
      case BookStatus.Finished: return 'Finished';
      case BookStatus.Confirmed: return 'Reviewed';
    }
    return 'None';
  }

  showReason(reason: string) {
    const config = new TemplateModalConfig<IDeclinedReasonContext, void, void>(this.reasonModal);
    config.context = {reason: reason};
    config.isInverted = true;
    config.size = ModalSize.Tiny;
    this.modalService.open(config);
  }

  toggleTaskList(id: number) {
    this.taskListOpened[id] = !this.taskListOpened[id];
  }

  decline(id: number) {
    this.reason = '';
    const config = new TemplateModalConfig<IDeclineConfirm, void, void>(this.declineTemplate);
    config.context = {id: id};
    config.isInverted = true;
    config.size = ModalSize.Tiny;
    this.currModal = this.modalService.open(config);
  }

  declineConfirm(id: number) {
    let book: CompanyTask = this.tasks.filter(b => b.Id == id)[0];
    book.Status = BookStatus.Declined;
    book.DeclinedReason = this.reason;
    this.loader = true;
    this.dashboardService.updateTask(book).then(resp => {
      this.loader = false;
      this.reason = null;
      this.loadData();
      this.currModal.deny(undefined);
      this.toastr.success('Declined task');
    }).catch(err => {
      this.loader = false;
      this.reason = null;
      this.currModal.deny(undefined);
      this.toastr.error('Ops. Cannot decline task');
    });
  }

  deleteBook(task: CompanyTask) {
    this.dloads[task.Id] = true;
    this.dashboardService.deleteCompanyTask(task.Id).then(resp => {
      this.toastr.success('Deleted task');
      return this.loadData();
    }).then(resp => {
      this.dloads[task.Id] = false;
    }).catch(err => {
      this.toastr.error('Ops. Cannot delete task');
      this.dloads[task.Id] = false;
    });
  }

  reassign(task: CompanyTask) {
    this.reassignTask = {
      Id: task.Id,
      BookId: task.ParentBookId,
      DeclinedReason: null,
      Description: null,
      WorkId: null,
      VendorId: null
    }
    this.selectedVendor = null;
    this.selectedWork = null;
    this.availableVendors = [];
    this.vendors.forEach(v => this.availableVendors.push(v));

    const config = new TemplateModalConfig<void, void, void>(this.reassignTemplate);
    config.isInverted = true;
    config.size = ModalSize.Tiny;
    this.currModal = this.modalService.open(config);
  }

  isVendorInBook(vendor: Vendor, task: CompanyTask): boolean {
    let result = false;
    this.tasks.filter(t => t.ParentBookId === task.ParentBookId)
      .forEach(t => {
        if (t.Vendor.Id === vendor.Id) {
          result = true;
        }
      });
    return result;
  }

  reassignConfirm() {
    debugger;
    this.reassignTask.VendorId = this.selectedVendor.Id;
    this.reassignTask.WorkId = this.selectedWork.Id;
    this.reassignLoader = true;
    this.dashboardService.reassignVendor(this.reassignTask).then(resp => {
      this.currModal.deny(undefined);
      this.loadData();
      this.reassignLoader = false;
      this.toastr.success('Vendor was successfully reassigned');
    })
    .catch(err => {
      this.reassignLoader = false;
      this.toastr.error('Ops. Cannot reassign vendor');
    });
  }

  leaveReview(id: number) {
    const config = new TemplateModalConfig<IReviewContext, void, void>(this.reviewTemplate);
    config.context = {id: id};
    config.isInverted = true;
    config.size = ModalSize.Tiny;
    this.currModal = this.modalService.open(config).onDeny(() => {
      this.review = {
        BookId: 0,
        Grade: 0,
        PerformerId: 0,
        PerformerType: '',
        Text: ''
      };
    });
  }

  leaveReviewConfirm(id: number) {
    if (this.review.Grade == 0) {
      return;
    }
    this.reviewLoader = true;
    let task = this.tasks.filter(t => t.Id === id)[0];
    this.review.BookId = id;
    this.review.PerformerId = task.Vendor.Id;
    this.review.PerformerType = 'vendor';
    this.reviewService.saveReview(this.review).then(resp => {
      return this.loadData();
    }).then(resp => {
      this.reviewLoader = false;
      this.currModal.deny(undefined);
      this.toastr.success('Vendor was successfully reviewed');
    }).catch(err => {
      this.reviewLoader = false;
      this.currModal.deny(undefined);
      this.toastr.error('Ops. Cannot review vendor');
    });
  }

  getVendorReviewAvatar(id: number): string {
    return this.tasks.filter(t => t.Id === id)[0].Vendor.Avatar;
  }

  getVendorReviewFio(id: number): string {
    let vendor = this.tasks.filter(t => t.Id === id)[0].Vendor as any;
    return vendor.FIO;
  }

  showFinish(id: number): boolean {
    return this.calcAllTasks(id) === this.calcConfirmedTasks(id);
  }

}

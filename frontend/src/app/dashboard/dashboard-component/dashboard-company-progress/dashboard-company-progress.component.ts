import { Component, OnInit, OnDestroy, ViewChild  } from '@angular/core';
import { DatePipe } from '@angular/common';

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';
import { CompanyTask } from '../../../models/book/book.model';
import { ShortTask } from '../../../models/dashboard/company-task';
import { Vendor } from '../../../models/company-page/vendor';
import { Work } from '../../../models/work.model';

import { DashboardService } from '../../../services/dashboard/dashboard.service';
import { DashMessagingService } from '../../../services/dashboard/dash-messaging.service';
import { NotificationService } from "../../../services/notifications/notification.service";

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

@Component({
  selector: 'app-dashboard-company-progress',
  templateUrl: './dashboard-company-progress.component.html',
  styleUrls: ['./dashboard-company-progress.component.sass']
})
export class DashboardCompanyProgressComponent implements OnInit, OnDestroy {

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
      this.books = resp;
      console.log(this.books);
    });
  }

  createTasks(id: number) {
    this.taskFormOpen[id] = true;
    this.someFormOpened = true;
    // let book: BookCard = this.books.filter(b => b.Id == id)[0];
    // book.Status = BookStatus.Finished;
    // this.loads[book.Id] = true;
    // this.dashboardService.update(book).then(resp => {
    //   this.dashboardEventsService.changeStatusToFinished();
    //   this.books.splice(this.books.findIndex(b => b.Id === id), 1);
    //   this.loads[book.Id] = false;
    //   this.dashMessaging.changeProgress();
    //   this.toastr.success('Finished task');
    // })
    // .catch(err => {
    //   this.loads[book.Id] = false;      
    //   this.toastr.error('Cannot finish task');
    // });
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
    return this.tasks.filter(t => t.ParentBookId === id && t.Status === BookStatus.Finished).length;
  }

  

  //vendor assign


  assignLoader: boolean;
  editMode: boolean;
  loader: boolean;
  reassignLoader: boolean;

  shortTasks: ShortTask[] = [];
  taskListOpened: {[bookId: number]: boolean} = {};

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
    this.currentTask = {
      Id: null,
      BookId: bookId,
      DeclinedReason: null,
      Description: null,
      WorkId: null,
      VendorId: null
    };
    this.taskFormOpened = true;
    this.selectedWork = null;
    this.selectedVendor = null;
    
  }

  saveTask(bookId: number) {
    this.currentTask.WorkId = this.selectedWork.Id;
    this.currentTask.VendorId = this.selectedVendor.Id;
    if (this.editMode) {
      
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
      this.shortTasks = [];
      this.restoreAvailableVendors();
      this.loadData().then(() => {
        this.assignLoader = false;
        this.assignedBooks[id] = true;
        this.taskFormOpen[id] = false;
        this.taskFormOpened = false;
        this.someFormOpened = false;
        this.editMode = false;
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

  getVendorIcon(task: ShortTask): string {
    debugger;
    let ven = this.vendors.filter(v => v.Id === task.VendorId)
    let ve = ven[0];
    return ve.Avatar;
  }

  editTask(task: ShortTask) {
    this.editMode = true;
    this.currentTask = task;
    this.selectedWork = this.works.filter(w => w.Id === task.WorkId)[0];
    let vendor = this.vendors.filter(v => v.Id === task.VendorId)[0];
    if (this.availableVendors.findIndex(v => v.Id === vendor.Id) == -1) {
      this.availableVendors.push(vendor);
    }
    this.selectedVendor = vendor;
    this.taskFormOpened = true;
  }

  deleteTask(task: ShortTask) {
    this.shortTasks.splice(this.shortTasks.findIndex(t => t.VendorId === task.VendorId), 1);
    this.restoreAvailableVendors();
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

}

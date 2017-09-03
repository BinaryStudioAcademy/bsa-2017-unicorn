import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CompanyDetails } from "../../../models/company-page/company-details.model";
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";
import { MapModel } from "../../../models/map.model";
import { CompanyWork } from "../../../models/company-page/company-work.model";
import { CompanyCategory } from "../../../models/company-page/company-category.model";
import { CompanySubcategory } from "../../../models/company-page/company-subcategory.model";
import { LocationService } from "../../../services/location.service";
import { NguiMapModule, Marker } from "@ngui/map";
import {ToastsManager, Toast} from 'ng2-toastr';

@Component({
  selector: 'app-company-main-information',
  templateUrl: './company-main-information.component.html',
  styleUrls: ['./company-main-information.component.sass']
})
export class CompanyMainInformationComponent implements OnInit {
  company: CompanyDetails;
  isLoaded: boolean = false;
  map: MapModel;  
  position;
  autocomplete: google.maps.places.Autocomplete;
  address: any = {};
  marker;
  @ViewChild('companyForm') public companyForm: NgForm;

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute, private LocationService: LocationService,
    private ref: ChangeDetectorRef,
    private toastr: ToastsManager
  ) { }
    markerDragged(event)
    {
      this.company.Location.Latitude = event.latLng.lat();
      this.company.Location.Longitude = event.latLng.lng();
    }
  ngOnInit() {
       
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyDetails(params['id'])).subscribe(res => {
      this.company = res;
      this.company.FoundationDate = new Date(this.company.FoundationDate);
      this.position={lat: this.company.Location.Latitude, lng: this.company.Location.Longitude};
      this.map = {
        center: {lat: this.company.Location.Latitude, lng: this.company.Location.Longitude},
        zoom: 18,    
        title: this.company.Name,
        label: this.company.Name,
        markerPos: {lat: this.company.Location.Latitude, lng: this.company.Location.Longitude}    
      };        
    });
  }

  save(){
    if (this.companyForm.invalid) {
      return;
    }
    this.isLoaded = true;
    this.LocationService.getLocDetails(this.company.Location.Latitude,this.company.Location.Longitude)
    .subscribe(
     result => {
        this.company.Location.Adress=(result.address_components[1].short_name+','+result.address_components[0].short_name)
        this.company.Location.City=result.address_components[3].short_name;
        this.companyService.saveCompanyDetails(this.company).then(() => {
          this.isLoaded = false;
          this.toastr.success('Changes were saved', 'Success!');
        })
        .catch(err => {
          this.toastr.error('Something goes wrong', 'Error!');
        });
     });
  }


  initialized(autocomplete: any) {
    this.autocomplete = autocomplete;
  }

  placeChanged(event) {
  //  let container = document.getElementById('autocomplete').textContent;
    this.company.Location.Latitude = event.geometry.location.lat();
    this.company.Location.Longitude = event.geometry.location.lng()
    this.position = {lat: this.company.Location.Latitude, lng: this.company.Location.Longitude}
    this.ref.detectChanges();
  }
}
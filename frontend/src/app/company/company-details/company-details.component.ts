import { Component, OnInit } from '@angular/core';
import { Company } from "../../models/company.model";
import { Review } from "../../models/review.model";

@Component({
  selector: 'app-company-details',
  templateUrl: './company-details.component.html',
  styleUrls: ['./company-details.component.css']
})
export class CompanyDetailsComponent implements OnInit {
company:Company = new Company;
  constructor() { }

  ngOnInit() {  
    this.company.avatar = "../../../assets/images/company_logo.png";
    this.company.name = "TURBOCAT 9000 Inc.";
    this.company.description = `Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
    Sed dignissim maximus fringilla. Ut tortor lectus, consequat sit amet ultricies ac,
     feugiat in libero. Sed augue diam, lacinia tincidunt sollicitudin sed, rhoncus id neque.
      Phasellus eu velit imperdiet, congue felis non, condimentum velit. Lorem ipsum dolor sit amet,
       consectetur adipiscing elit. Mauris pretium arcu vitae mauris rutrum, et tempor est congue.`;
    this.company.foundationDate = new Date(Date.now());
    this.company.location = "Lviv, Ukraine";
    this.company.reviews = [
      {
        avatar: "../../../assets/images/square-image.png",
        date: new Date(Date.now()),
        from: "Anton",
        to: "TURBOCAT 9000 Inc.",
        grade: 4,
        description: "Good, good"
      },
      {
        avatar: "../../../assets/images/square-image.png",
        date: new Date(Date.now()),
        from: "Egor",
        to: "TURBOCAT 9000 Inc.",
        grade: 5,
        description: "Very good"
      },
      {
        avatar: "../../../assets/images/square-image.png",
        date: new Date(Date.now()),
        from: "Kolya",
        to: "TURBOCAT 9000 Inc.",
        grade: 5,
        description: "Excellent"
      }
    ];
    this.company.rating = 4;
    this.company.vendors = [
    {
      avatar:"../../../assets/images/square-image.png",
      experience: "3 month",
      exWork: "none",
      position: "Worker",
      fio:"Los' Losevich"
    },
    {
      avatar:"../../../assets/images/square-image.png",
      experience: "3 years",
      exWork: "Microsoft",
      position: "Middle .net developer",
      fio:"Gus' Gusevich"
    },
    {
      avatar:"../../../assets/images/square-image.png",
      experience: "7 years",
      exWork: "Google",
      position: "Cleaner",
      fio:"Kon' Konevich"
    }];
    this.company.director = "Vladik";
    this.company.features = ["Excellent service", "We are going fast", "We've scratched cats since 1997", "Warm hands"];
  } 

}

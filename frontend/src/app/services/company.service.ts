import { Injectable } from '@angular/core';
import { DataService } from "./data.service";
import { Company } from "../models/company.model";
import { environment } from "../../environments/environment";

@Injectable()
export class CompanyService { 
  result: Company;
  constructor(private dataService: DataService) { }

  getCompany(id: number):Promise<Company>{
    return this.dataService.getRequest<Company>(environment.apiUrl + "company/" + id)
    .then(res => { return res }); 
      
    

    
    // return { 
    //   avatar: "../../../assets/images/company_logo.png",
    //   name: "TURBOCAT 9000 Inc.",
    //   description: `Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
    //     Sed dignissim maximus fringilla. Ut tortor lectus, consequat sit amet ultricies ac,
    //       feugiat in libero. Sed augue diam, lacinia tincidunt sollicitudin sed, rhoncus id neque.
    //         Phasellus eu velit imperdiet, congue felis non, condimentum velit. Lorem ipsum dolor sit amet,
    //           consectetur adipiscing elit. Mauris pretium arcu vitae mauris rutrum, et tempor est congue.`,
    //   foundationDate: new Date(Date.now()),
    //   location: {
    //     adress: "Chernovola, 59",
    //     city: "Lviv, Ukraine",
    //     lat: 49.85711,
    //     lng: 24.01980},
    //   reviews: [
    //   {
    //     avatar: "../../../assets/images/square-image.png",
    //     date: new Date(Date.now()),
    //     from: "AntonAntonAnton",
    //     to: "TURBOCAT 9000 Inc.",
    //     grade: 4,
    //     description: "Good, good"
    //   },
    //   {
    //     avatar: "../../../assets/images/square-image.png",
    //     date: new Date(Date.now()),
    //     from: "Egor",
    //     to: "TURBOCAT 9000 Inc.",
    //     grade: 5,
    //     description: "Very good"
    //   },
    //   {
    //     avatar: "../../../assets/images/square-image.png",
    //     date: new Date(Date.now()),
    //     from: "Kolya",
    //     to: "TURBOCAT 9000 Inc.",
    //     grade: 5,
    //     description: "Excellent"
    //   }
    //   ],
    //   rating: 4,
    //   vendors: [
    //   {
    //     avatar:"../../../assets/images/square-image.png",
    //     experience: "3 month",
    //     position: "Worker",
    //     fio:"Los' Losevich"
    //   },
    //   {
    //     avatar:"../../../assets/images/square-image.png",
    //     experience: "3 years",
    //     position: "Middle .net developer",
    //     fio:"Gus' Gusevich"
    //   },
    //   {
    //     avatar:"../../../assets/images/square-image.png",
    //     experience: "7 years",
    //     position: "Cleaner",
    //     fio:"Kon' Konevich"
    //   },
    //   {
    //     avatar:"../../../assets/images/square-image.png",
    //     experience: "7 years",
    //     position: "Cleaner",
    //     fio:"Kon' Konevich"
    //   },
    //   {
    //     avatar:"../../../assets/images/square-image.png",
    //     experience: "7 years",
    //     position: "Cleaner",
    //     fio:"Kon' Konevich"
    //   },
    //   {
    //     avatar:"../../../assets/images/square-image.png",
    //     experience: "3 years",
    //     position: "Middle .net developer",
    //     fio:"Gus' Gusevich"
    //   }],
    //   director: "Vladik",
    //   categories: [
    //     {
    //       icon: "../../../assets/images/square-image.png",
    //       category: "Excellent service"
    //     },
    //     {
    //       icon: "../../../assets/images/square-image.png",
    //       category: "We are going fast"
    //     },
    //     {
    //       icon: "../../../assets/images/square-image.png",
    //       category: "We've scratched cats since 1997"
    //     },{
    //       icon: "../../../assets/images/square-image.png",
    //       category: "Warm hands"
    //     }]
    // };
  }

}

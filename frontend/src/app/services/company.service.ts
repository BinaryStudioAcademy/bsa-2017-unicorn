import { Injectable } from '@angular/core';
import { DataService } from "./data.service";
import { Company } from "../models/company.model";
import { environment } from "../../environments/environment";

@Injectable()
export class CompanyService { 
  result: Company;
  constructor(private dataService: DataService) {
    dataService.setHeader('Content-Type', 'application/json');
   }

  getCompany(id: number):Promise<Company>{
    return this.dataService.getRequest<Company>("company/" + id)
    .then(res => { return res });    
  }

  saveCompany(company: Company):Promise<Company>{
    return this.dataService.postRequest("company", company);
  }

  getMockCompany(): Company{
    return { 
      Id: 1,
      Avatar: "../../../assets/images/company_logo.png",
      Name: "TURBOCAT 9000 Inc.",
      Description: `Lorem ipsum dolor sit amet, consectetur adipiscing elit.
        Sed dignissim maximus fringilla. Ut tortor lectus, consequat sit amet ultricies ac,
          feugiat in libero. Sed augue diam, lacinia tincidunt sollicitudin sed, rhoncus id neque.
            Phasellus eu velit imperdiet, congue felis non, condimentum velit. Lorem ipsum dolor sit amet,
              consectetur adipiscing elit. Mauris pretium arcu vitae mauris rutrum, et tempor est congue.`,
      FoundationDate: new Date(Date.now()),
      Location: {
        Adress: "Chornovola 2",
        City: "Lviv",
        Latitude: Math.random() * 0.0099 + 43.7250,
        Longitude: Math.random() * 0.0099 + -79.7699,
      },
      Reviews: [
      {
        Id: 1,
        Avatar: "../../../assets/images/square-image.png",
        Date: new Date(Date.now()),
        FromAccountId: 1,
        From: "AntonAntonAnton",
        ToAccountId: 1,
        To: "TURBOCAT 9000 Inc.",
        Grade: 4,
        Description: "Good, good",
        BookId: 2,
      },
      {
        Id: 2,
        Avatar: "../../../assets/images/square-image.png",
        Date: new Date(Date.now()),
        FromAccountId: 1,
        From: "AntonAntonAnton",
        ToAccountId: 1,
        To: "TURBOCAT 9000 Inc.",
        Grade: 4,
        Description: "Good, good",
        BookId: 2,
      },
      {
        Id: 3,
        Avatar: "../../../assets/images/square-image.png",
        Date: new Date(Date.now()),
        FromAccountId: 1,
        From: "AntonAntonAnton",
        ToAccountId: 1,
        To: "TURBOCAT 9000 Inc.",
        Grade: 4,
        Description: "Good, good",
        BookId: 2,
      },
      ],
      Rating: 4,
      Vendors: [
      {
        Avatar: "../../../assets/images/square-image.png",
        Experience: 5,
        Position: '',
        FIO: 'Vasya Pupkin',
      }, {
        Avatar: "../../../assets/images/square-image.png",
        Experience: 5,
        Position: '',
        FIO: 'Vasya Pupkin',
      }, {
        Avatar: "../../../assets/images/square-image.png",
        Experience: 5,
        Position: '',
        FIO: 'Vasya Pupkin',
      }
      ],
      Director: "Vladik",
      Categories: [{
        Icon: "../../../assets/images/square-image.png",
        Name: "Category"
      }],
      Contacts: [
        {
          Id: 1,
          Value: "+380666666666",
          Type: "phone",
          Provider: "phone"
        }
      ]
    };
  }

}

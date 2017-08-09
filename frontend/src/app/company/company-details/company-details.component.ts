import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-company-details',
  templateUrl: './company-details.component.html',
  styleUrls: ['./company-details.component.css']
})
export class CompanyDetailsComponent implements OnInit {
  selectMenu: string = "General information";

  selectors = [
    { name:"General information", active:"active"},
    { name:"Reviews", active:""},
    { name:"Vendors", active:""},
    { name:"Contacts", active:""}];
  
  mockCompany = {
    name:"TURBOCAT 9000 Inc.",
    description:`Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
    Sed dignissim maximus fringilla. Ut tortor lectus, consequat sit amet ultricies ac,
     feugiat in libero. Sed augue diam, lacinia tincidunt sollicitudin sed, rhoncus id neque.
      Phasellus eu velit imperdiet, congue felis non, condimentum velit. Lorem ipsum dolor sit amet,
       consectetur adipiscing elit. Mauris pretium arcu vitae mauris rutrum, et tempor est congue.`,
    foundationDate: Date.now,    
    location: "Lviv, Ukraine",
    reviews: ["review1","review2","review3"],
    rating: 4.5,
    vendors: ["vendor1", "vendor2", "vendor3"],
    director: "Vladik",
    features: ["Excellent service", "We are going fast", "We've scratched cats since 1997", "Warm hands"]    
  }
  

  constructor() { }

  ngOnInit() {    
  }

  changeSelector(selector: any)  {
    selector.active = "active";
    for(let item of this.selectors){
      if(item != selector){
        item.active = "";
      }
    }
    this.selectMenu = selector.name;    
  }

}

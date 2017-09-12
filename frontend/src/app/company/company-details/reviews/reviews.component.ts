import { Component, OnInit } from '@angular/core';
import { Review } from "../../../models/review.model";
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";
import { CompanyReviews } from "../../../models/company-page/company-reviews.model";

@Component({
  selector: 'company-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.sass']
})
export class ReviewsComponent implements OnInit {
company: CompanyReviews;
isReviewsEmpty: boolean = true;
isLoaded: boolean;
constructor(private companyService: CompanyService,
  private route: ActivatedRoute) { }

  ngOnInit() {       
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyReviews(params['id']))
    .subscribe(res => {
      this.isLoaded = true;
      if(res !== null){
        if(res.Reviews.length == 0){
          this.isReviewsEmpty = true;
        }
        else{
          this.isReviewsEmpty = false;
        }
        this.company = res;
        console.log(res);
      }
    });   
  }

}

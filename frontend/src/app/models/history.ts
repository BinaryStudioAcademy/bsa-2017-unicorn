import {User} from './user'
import {Review} from './review.model'
import { Vendor } from "./company-page/vendor";

export interface History {
   date : Date 
   dateFinished: Date 
   bookDescription: string 
   workDescription: string 
   vendor: string
   categoryName: string 
   subcategoryName: string  
}
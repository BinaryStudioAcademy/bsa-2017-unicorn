import {Vendor} from './vendor'
import {User} from './user'
import {Review} from './review.model'

export interface History {
   date : Date 
   dateFinished: Date 
   bookDescription: string 
   workDescription: string 
   vendor: Vendor 
   categoryName: string 
   subcategoryName: string  
}
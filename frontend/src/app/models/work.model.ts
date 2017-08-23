import { Category } from "./category.model";
import { Subcategory } from "./subcategory.model";

export interface Work {
	Id: number;
	Name: string;
	Icon: string;
	Category: string;
	CategoryId: number;
	Subcategory: string;
	SubcategoryId: number;
	Description: string;	
}
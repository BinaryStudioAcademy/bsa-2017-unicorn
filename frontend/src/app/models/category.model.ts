import { Subcategory } from "./subcategory.model";

export class Category
{
	Id: number;
	Name: string;
	Description: string;
	Icon: string;
	Subcategories: Subcategory[];
}
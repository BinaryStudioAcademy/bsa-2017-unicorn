import { Performer } from "../performer.model";

export interface PerformersPage {
	Items: Performer[];
	CurrentPage: number;
	TotalCount: number;
	PageSize: number;
} 
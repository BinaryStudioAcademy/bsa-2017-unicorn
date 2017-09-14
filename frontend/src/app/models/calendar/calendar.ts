import { ExtraDayModel } from "./extra-day";
import { VendorBook } from "../book/vendor-book.model";

export interface CalendarModel {
    Id: number;
    StartDate: Date;
    EndDate: Date;
    WorkOnWeekend: boolean;
    SeveralTasksPerDay: boolean;
    ExtraDayOffs: ExtraDayModel[];
    ExtraWorkDays: ExtraDayModel[];
    Events: VendorBook[];
}
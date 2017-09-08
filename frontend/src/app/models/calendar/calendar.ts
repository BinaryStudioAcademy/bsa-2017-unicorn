import { ExtraDayModel } from "./extra-day";

export interface CalendarModel {
    Id: number;
    StartDate: Date;
    EndDate: Date;
    WorkOnWeekend: boolean;
    ExtraDayOffs: ExtraDayModel[];
    ExtraWorkDays: ExtraDayModel[];
}
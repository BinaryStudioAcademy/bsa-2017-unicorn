import { ReportType } from './reportType.model';

export interface Report {
    Id: number;
    Date: Date;
    Type: ReportType;
    Message: string;
    Email: string;
    ProfileId: number;
    ProfileName: string;
    ProfileType: string;
}

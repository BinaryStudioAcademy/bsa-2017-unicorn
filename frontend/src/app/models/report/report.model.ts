import { ReportType } from './reportType.model';
export interface Report {
    Id: number;
    Date: Date;
    Type: ReportType;
    Message: string;
    Email: string;
    CustomerId: number;
    VendorId: number;
    CompanyId: number;
}

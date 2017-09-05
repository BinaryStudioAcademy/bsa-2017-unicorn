export interface Review {
    Id: number;
    Avatar: string;
    Date: Date;
    FromAccountId: number;
    FromProfileId: number;
    From: string;
    ToAccountId: number;
    To: string;
    Description: string;
    BookId: number;
    Grade: number;
    WorkName: string;
}
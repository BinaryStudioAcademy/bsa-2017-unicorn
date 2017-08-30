export interface Review {
    Id: number;
    Avatar: string;
    Date: Date;
    FromAccountId: number;
    From: string;
    ToAccountId: number;
    To: string;
    Description: string;
    BookId: number;
    Grade: number;
    WorkName: string;
}
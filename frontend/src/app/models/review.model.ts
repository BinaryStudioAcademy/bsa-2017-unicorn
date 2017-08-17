export interface Review {
    Id: number
    Avatar: string;
    Date: Date;
    FromAccountId: number;
    From: string;
    ToAccountId: number;
    To: string;
    Grade: number;
    Description: string;
    BookId: number;
}
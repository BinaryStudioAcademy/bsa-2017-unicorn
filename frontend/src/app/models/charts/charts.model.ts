export interface BooksAccepted {
    name: string;
    series: Point[];
}

export interface BooksDeclined {
    name: string;
    series: Point[];
}

export interface Point {
    name: string;
    value: number;
}

export interface PopularWorks {
    Points: Point[];
}

export interface ConfirmedWorks {
    Points: Point[];
}

export interface DeclinedWorks {
    Points: Point[];
}

export interface Analytics {
    BooksAccepted: BooksAccepted;
    BooksDeclined: BooksDeclined;
    PopularWorks: PopularWorks;
    ConfirmedWorks: ConfirmedWorks;
    DeclinedWorks: DeclinedWorks;
    VendorsByRating: DeclinedWorks;
    VendorsByOrders: DeclinedWorks;
    VendorsByFinished: DeclinedWorks;
}
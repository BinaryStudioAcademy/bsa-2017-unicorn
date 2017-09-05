export interface Company {
    Id: number;
    Avatar: string;
    Name: string;
    Description?: any;
    FoundationDate: Date;
    Rating: number;
    Director?: any;
    Reviews?: any;
    Vendors?: any;
    Categories?: any;
    Contacts?: any;
    Account?: any;
}

export interface Offer {
    Id: number;
    AttachedMessage: string;
    DeclinedMessage?: any;
    Status: OfferStatus;
    Company: Company;
    Vendor?: any;
}

export enum OfferStatus {
    Pending,
    Accepted,
    Declined
}
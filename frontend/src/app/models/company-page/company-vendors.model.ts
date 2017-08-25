import { Vendor } from "./vendor";

export interface CompanyVendors {
    Id: number;
    // Vendors: {Result: Vendor[]};   
    // AllVendors: {Result: Vendor[]};  
    Vendors: Vendor[];
    AllVendors: Vendor[];
    
}
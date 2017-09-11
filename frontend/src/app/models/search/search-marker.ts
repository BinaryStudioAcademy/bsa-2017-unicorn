import { SearchWork } from './search-work';
export interface SearchMarker {
    name: string;
    latitude: number;
    longitude: number;
    works: SearchWork[];
  }

import { EmailValidator, MinLengthValidator } from "@angular/forms";

export interface IReadCrime {
  id: string;
  crimeType: string;
  description: string;
  crimeReportStatus: string;
  placeOfEvent: string;
  assignedLawEnforcmentId: number;
  createdDate: Date
}

export interface IPostCrime {
  crimeType: CrimeEventType;
  description: string;
  placeOfEvent: string;
  email?: string;
}

export enum CrimeEventType {
  Assault,
  Burglary,
  Littering
}

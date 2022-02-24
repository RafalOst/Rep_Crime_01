import { Crime_listService } from '../Service/crime_list.service';
import { Component, OnInit } from '@angular/core';
import { IReadCrime } from '../ICrime';

@Component({
  selector: 'app-crimes_list',
  templateUrl: './crimes_list.component.html',
  styleUrls: ['./crimes_list.component.css']
})
export class Crimes_listComponent implements OnInit {
  crimeList: IReadCrime[] | any = "";

  constructor(
    private Crime_listService: Crime_listService,
  ) { }

  ngOnInit() {
    this.getCrimes();
  }

  getCrimes(){
    this.Crime_listService.showCrimes().subscribe((result)=> {this.crimeList = result;})

    // usunac po testach - corse policy blokuje dostep
    //this.crimeList = [{"id":"602d2149e773f2a3990b47f5","crimeType":"Littering","description":"Obywatel Kot wyrzuca smieci na ulice","crimeReportStatus":"Waiting","placeOfEvent":"Krakow, ul. Kocia 7","assignedLawEnforcmentId":1,"createdDate":"2022-02-23T11:52:06.719Z"},{"id":"602d2149e773f2a3990b47f4","crimeType":"Assault","description":"Obywatel Kot napadl na obywatela Psa","crimeReportStatus":"Waiting","placeOfEvent":"Krakow, ul. Psia 6","assignedLawEnforcmentId":1,"createdDate":"2022-02-23T11:52:06.719Z"},{"id":"602d2149e773f2a3990b47f3","crimeType":"Burglary","description":"Obywatel Kot wlamal sie do obywatela Ptaka","crimeReportStatus":"Waiting","placeOfEvent":"Krakow, ul. Ptasia 3/2","assignedLawEnforcmentId":1,"createdDate":"2022-02-23T11:52:06.719Z"},{"id":"621620ee99116ba201e02600","crimeType":"Littering","description":"testtttttttttttttttttttt k8s numer 2test ","crimeReportStatus":"Waiting","placeOfEvent":"testowa ulica k8s","assignedLawEnforcmentId":1,"createdDate":"2022-02-23T11:56:30.191Z"},{"id":"6216326a99116ba201e02601","crimeType":"Littering","description":"testtttttttttttttttttttt k8s numer 2test??????????? ","crimeReportStatus":"Waiting","placeOfEvent":"testowa ulica k8s","assignedLawEnforcmentId":2,"createdDate":"2022-02-23T13:11:06.556Z"}]
  }

}

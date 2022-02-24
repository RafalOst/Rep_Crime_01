import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Crime_listService } from '../Service/crime_list.service';

@Component({
  selector: 'app-report_crime',
  templateUrl: './report_crime.component.html',
  styleUrls: ['./report_crime.component.css']
})

export class Report_crimeComponent implements OnInit {

  newCrimeForm = new FormGroup({
    email: new FormControl(),
    crimeType: new FormControl(null, Validators.required),
    placeOfEvent: new FormControl(null, Validators.required),
    description: new FormControl(null, Validators.required),
  });

  constructor(private Crime_listService: Crime_listService, ) { }

  ngOnInit() {
  }


  addCrime(): void {
    if (this.newCrimeForm.valid) {
        alert("Crime reported!")
        console.log(this.newCrimeForm);
        this.Crime_listService.postCrime(this.newCrimeForm.value)
          .subscribe(data => {
            console.log(data);
          })
        this.newCrimeForm.reset();
    }
  }
}

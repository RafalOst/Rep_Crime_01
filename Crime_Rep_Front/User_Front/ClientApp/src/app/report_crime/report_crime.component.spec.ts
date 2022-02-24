/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { Report_crimeComponent } from './report_crime.component';

describe('Report_crimeComponent', () => {
  let component: Report_crimeComponent;
  let fixture: ComponentFixture<Report_crimeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Report_crimeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Report_crimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

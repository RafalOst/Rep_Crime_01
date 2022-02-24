/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { Crimes_listComponent } from './crimes_list.component';

describe('Crimes_listComponent', () => {
  let component: Crimes_listComponent;
  let fixture: ComponentFixture<Crimes_listComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Crimes_listComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Crimes_listComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

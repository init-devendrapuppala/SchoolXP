import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AddEditStudent } from './add-edit-student';

describe('AddEditStudent', () => {
  let component: AddEditStudent;
  let fixture: ComponentFixture<AddEditStudent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddEditStudent],
    }).compileComponents();

    fixture = TestBed.createComponent(AddEditStudent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

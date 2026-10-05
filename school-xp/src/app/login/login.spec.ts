import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { Login } from './login';

describe('Login', () => {
  let component: Login;
  let fixture: ComponentFixture<Login>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Login],
      providers: [provideRouter([])],
    }).compileComponents();

    fixture = TestBed.createComponent(Login);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should switch between login and signup views', () => {
    expect(component.mode).toBe('login');

    component.showSignUp();
    expect(component.mode).toBe('signup');

    component.showLogin();
    expect(component.mode).toBe('login');
  });

  it('should enter the confirmation state', () => {
    component.mode = 'confirm';
    expect(component.mode).toBe('confirm');
  });
});

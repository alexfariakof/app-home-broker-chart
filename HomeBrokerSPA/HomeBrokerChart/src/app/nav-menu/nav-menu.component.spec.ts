import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NavMenuComponent } from './nav-menu.component';

describe('Test Unit NavMenuComponent', () => {
  let component: NavMenuComponent;
  let fixture: ComponentFixture<NavMenuComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NavMenuComponent]
    });

    fixture = TestBed.createComponent(NavMenuComponent);
    component = fixture.componentInstance;
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize isExpanded to false', () => {
    expect(component.isExpanded).toBeFalse();
  });

  it('should toggle isExpanded correctly', () => {
    expect(component.isExpanded).toBeFalse();
    component.toggle();
    expect(component.isExpanded).toBeTrue();
    component.toggle();
    expect(component.isExpanded).toBeFalse();
  });

  it('should collapse isExpanded correctly', () => {
    component.isExpanded = true;
    component.collapse();
    expect(component.isExpanded).toBeFalse();
  });
});

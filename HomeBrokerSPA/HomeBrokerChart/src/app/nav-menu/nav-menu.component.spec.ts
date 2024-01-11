import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NavMenuComponent } from './nav-menu.component';

describe('Test Unit NavMenuComponent', () => {
  let component: NavMenuComponent;
  let fixture: ComponentFixture<NavMenuComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NavMenuComponent],
    });

    fixture = TestBed.createComponent(NavMenuComponent);
    component = fixture.componentInstance;
  });

  it('should create NavMenuComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should collapse', () => {
    // Act
    component.collapse();

    // Assert
    expect(component.isExpanded).toBeFalsy();
  });

  it('should toggle', () => {
    // Arrange
    component.isExpanded = false;

    // Act
    component.toggle();

    // Assert
    expect(component.isExpanded).toBeTruthy();

    // Act
    component.toggle();

    // Assert
    expect(component.isExpanded).toBeFalsy();
  });
});

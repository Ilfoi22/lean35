import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeletedTodosComponent } from './deleted-todos.component';

describe('DeletedTodosComponent', () => {
  let component: DeletedTodosComponent;
  let fixture: ComponentFixture<DeletedTodosComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeletedTodosComponent]
    });
    fixture = TestBed.createComponent(DeletedTodosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

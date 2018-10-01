import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlbumSetComponent } from './album-set.component';

describe('AlbumSetComponent', () => {
  let component: AlbumSetComponent;
  let fixture: ComponentFixture<AlbumSetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlbumSetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlbumSetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

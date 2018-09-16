import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlbumsViewerComponent } from './albums-viewer.component';

describe('AlbumsViewerComponent', () => {
  let component: AlbumsViewerComponent;
  let fixture: ComponentFixture<AlbumsViewerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlbumsViewerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlbumsViewerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

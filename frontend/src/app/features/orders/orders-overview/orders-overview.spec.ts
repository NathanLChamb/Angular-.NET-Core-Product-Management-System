import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrdersOverview } from './orders-overview';

describe('OrdersOverview', () => {
  let component: OrdersOverview;
  let fixture: ComponentFixture<OrdersOverview>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrdersOverview]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrdersOverview);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

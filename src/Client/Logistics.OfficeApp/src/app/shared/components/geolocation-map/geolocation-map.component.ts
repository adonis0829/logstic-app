import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import {NgFor, NgIf} from '@angular/common';
import {MarkerComponent, NgxMapboxGLModule} from 'ngx-mapbox-gl';
import {TruckGeolocation} from '@core/models';


@Component({
  selector: 'app-geolocation-map',
  templateUrl: './geolocation-map.component.html',
  styleUrls: ['./geolocation-map.component.scss'],
  standalone: true,
  imports: [
    NgxMapboxGLModule,
    NgFor,
    NgIf,
  ],
})
export class GeolocationMapComponent implements OnInit, OnChanges {
  public selectedMarker: Marker | null = null;

  @Input({required: true}) accessToken!: string;
  @Input() geolocationData: TruckGeolocation[] = [];
  @Input() zoom: number = 3;
  @Input() center: [number, number] = [-95, 35];

  ngOnInit(): void {}
  ngOnChanges(changes: SimpleChanges): void {}

  trackByFn(index: number, item: TruckGeolocation): string {
    return item.truckId;
  }

  onSelectMarker(geoData: TruckGeolocation, markerComponent: MarkerComponent): void {
    this.selectedMarker = {
      component: markerComponent,
      data: geoData,
    };
  }
}

interface Marker {
  component: MarkerComponent;
  data: TruckGeolocation;
}

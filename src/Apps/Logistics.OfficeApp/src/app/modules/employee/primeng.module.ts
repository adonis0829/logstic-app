import { NgModule } from '@angular/core';
import { CardModule } from 'primeng/card';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { TableModule } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';

@NgModule({
  exports: [
    CardModule,
    ProgressSpinnerModule,
    TableModule,
    AutoCompleteModule,
    DropdownModule
  ]
})
export class PrimengModule { }

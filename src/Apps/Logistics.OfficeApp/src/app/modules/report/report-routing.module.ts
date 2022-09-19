import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../auth/auth.guard';
import { ReportPageComponent } from './pages/overview/overviewcomponent';
import { TruckReportComponent } from './pages/truck-report/truck-report.component';

const rootRoutes: Routes = [
  {
    path: '',
    redirectTo: 'overview',
    pathMatch: 'full'
  },
  { 
    path: 'overview', 
    component: ReportPageComponent, 
    canActivate: [AuthGuard],
    data: {
      breadcrumb: 'Overview',
      roles: ['app.admin', 'tenant.owner', 'tenant.manager']
    }
  },
  { 
    path: 'truck/:id', 
    component: TruckReportComponent, 
    canActivate: [AuthGuard],
    data: {
      breadcrumb: 'Truck',
      roles: ['app.admin', 'tenant.owner', 'tenant.manager']
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(rootRoutes)],
  exports: [RouterModule],
})
export class ReportRoutingModule {}
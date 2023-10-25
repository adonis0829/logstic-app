import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterLink} from '@angular/router';
import {TooltipModule} from 'primeng/tooltip';
import {SplitButtonModule} from 'primeng/splitbutton';
import {PanelMenuModule} from 'primeng/panelmenu';
import {OverlayPanelModule} from 'primeng/overlaypanel';
import {MenuItem} from 'primeng/api';
import {AppConfig} from '@configs';
import {AuthService} from '@core/auth';
import {ApiService, TenantService} from '@core/services';


@Component({
  selector: 'app-sidebar',
  standalone: true,
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
  imports: [
    CommonModule,
    RouterLink,
    TooltipModule,
    SplitButtonModule,
    PanelMenuModule,
    OverlayPanelModule,
  ],
})
export class SidebarComponent implements OnInit {
  public isAuthenticated: boolean;
  public isLoading: boolean;
  public isOpened: boolean;
  public companyName?: string;
  public userRole?: string | null;
  public userFullName?: string;
  public profileMenuItems: MenuItem[];
  public accountingMenuItems: MenuItem[];

  constructor(
    private authService: AuthService,
    private apiService: ApiService,
    private tenantService: TenantService)
  {
    this.isAuthenticated = false;
    this.isOpened = false;
    this.isLoading = false;
    this.profileMenuItems = [
      {
        label: 'User name',
        icon: 'bi bi-person-circle',
        items: [
          {
            label: 'Profile',
            command: () => this.openAccountUrl(),
          },
          {
            separator: true,
          },
          {
            label: 'Sign out',
            command: () => this.logout(),
          },
        ],
      },
    ];

    this.accountingMenuItems = [
      {
        label: 'Accounting',
        icon: 'bi bi-journal-text h1',
        items: [
          {
            label: 'Payroll Management',
            // command: () => this.openAccountUrl(),
          },
          {
            label: 'Payments',
            // command: () => this.logout(),
          },
          {
            label: 'Invoices',
            // command: () => this.logout(),
          },
        ],
      },
    ];
  }

  ngOnInit(): void {
    this.authService.onUserDataChanged().subscribe((userData) => {
      this.userFullName = userData?.getFullName();
      this.userRole = this.authService.getUserRoleName();
      this.profileMenuItems[0].label = this.userFullName;
      this.fetchTenantData();
    });
  }

  private fetchTenantData() {
    this.apiService.getTenant().subscribe((result) => {
      if (result.isError || !result.data) {
        return;
      }

      this.tenantService.setTenantData(result.data);
      this.companyName = result.data.companyName;
    });
  }

  toggle() {
    this.isOpened = !this.isOpened;
  }

  logout() {
    this.authService.logout();
  }

  openAccountUrl() {
    window.open(`${AppConfig.idHost}/account/manage/profile`, '_blank');
  }
}

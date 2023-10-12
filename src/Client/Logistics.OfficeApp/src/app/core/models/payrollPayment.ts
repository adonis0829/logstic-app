import {Employee} from './employee';
import {Payment} from './payment';


export interface PayrollPayment {
  startDate: string;
  endDate: string;
  payment: Payment;
  employee?: Employee; 
}

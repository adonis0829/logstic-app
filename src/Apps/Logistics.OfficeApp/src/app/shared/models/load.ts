export interface Load {
  id?: string;
  refId?: number;
  name?: string;
  sourceAddress?: string;
  destinationAddress?: string;
  deliveryCost?: number;
  distance?: number;
  status?: string;
  dispatchedDate?: Date;
  pickUpDate?: Date;
  deliveryDate?: Date;
  assignedDispatcherId?: string;
  assignedDispatcherName?: string;
  assignedTruckId?: string;
  assignedDriverId?: string;
  assignedDriverName?: string;
}
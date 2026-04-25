import { HubConnectionBuilder } from '@microsoft/signalr';

export const setupSignalRConnection = (url) => {
  return new HubConnectionBuilder()
    .withUrl(url, {
      withCredentials: true,
    })
    .withAutomaticReconnect()
    .build();
};
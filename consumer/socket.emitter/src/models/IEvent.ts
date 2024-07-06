export default interface IEvent {
    Body: any;
    From: string | null;
    Template: string;
    To: string | null;
}
interface Props {
  message: string;
}

export default function Error({ message }: Props) {
  return (
    <div
      className="alert alert-danger m-5 d-flex justify-content-between align-items-center"
      role="alert"
    >
      <p>{message}</p>
    </div>
  );
}

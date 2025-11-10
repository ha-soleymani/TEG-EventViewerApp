interface Props {
  resourceName?: string;
}

export default function Loading({ resourceName }: Props) {
  return (
    <div className="d-flex justify-content-center my-4">
      <div className="spinner-border" role="status">
        <span className="visually-hidden">
          Loading {resourceName ?? ""} ...
        </span>
      </div>
    </div>
  );
}

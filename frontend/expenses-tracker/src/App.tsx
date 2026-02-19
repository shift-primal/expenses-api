import { useQuery } from "@tanstack/react-query";

function App() {
  const baseUrl = "http://localhost:5071";

  const { data, isLoading } = useQuery({
    queryKey: ["transactions"],
    queryFn: () =>
      fetch(`${baseUrl}/transactions?pageNumber=1`).then((r) => r.json()),
  });

  return (
    <>
      <button
        className="border-2 border-red-400"
        onClick={() => console.log(data)}
      >
        Hey
      </button>
    </>
  );
}

export default App;

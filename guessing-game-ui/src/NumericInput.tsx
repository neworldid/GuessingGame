import TextField from '@mui/material/TextField';

interface NumericInputProps {
	onChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

export default function NumericInput({ onChange }: NumericInputProps) {
	return (
		<div className="p-4">
			<TextField
				type="number"
				label="Numeric Input"
				variant="outlined"
				className="w-full"
				inputProps={{ min: 1, step: 1 }}
				onChange={onChange}
			/>
		</div>
	);
}
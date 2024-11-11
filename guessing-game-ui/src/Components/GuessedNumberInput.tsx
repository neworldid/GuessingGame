import TextField from '@mui/material/TextField';

interface NumericInputProps {
	value: string;
	onChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
	errorMessage: string;
}

export default function GuessedNumberInput({ value, onChange, errorMessage }: NumericInputProps) {
	return (
		<div className="p-4">
			<TextField
				type="number"
				label="Numeric Input"
				variant="outlined"
				value={value}
				className="w-full"
				inputProps={{ min: 1, step: 1 }}
				onChange={onChange}
				error={errorMessage?.length > 0}
				helperText={errorMessage}
			/>
		</div>
	);
}
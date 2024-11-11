import TextField from '@mui/material/TextField';

interface NameInputProps {
	value: string;
	onChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
	errorMessage: string;
}

export default function NameInput({ value, onChange, errorMessage }: NameInputProps) {
	return (
		<div className="p-4">
			<TextField
				label="Input your name here"
				variant="outlined"
				value={value}
				className="w-full"
				onChange={onChange}
				error={errorMessage.length > 0}
				helperText={errorMessage}
			/>
		</div>
	);
}
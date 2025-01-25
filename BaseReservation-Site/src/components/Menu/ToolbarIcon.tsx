import { Box } from "@mui/material"

export const ToolbarIcon = () => {
    return (
        <Box
            sx={{
                width: 50,
                height: 50,
                backgroundColor: '#DB9F6A',
                clipPath: 'polygon(50% 0%, 70% 35%, 100% 50%, 70% 65%, 50% 100%, 30% 65%, 0% 50%, 30% 35%)',
            }}
        />)
}
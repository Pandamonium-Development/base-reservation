import { ListItemText, MenuItem } from "@mui/material"
import { useNavigate } from "react-router-dom";

export const ToolbarOption = ({ OptionName, OptionPath }: { OptionName: string, OptionPath: string }) => {
    const navigate = useNavigate();

    return (
        <MenuItem sx={{ color: 'text.primary' }} onClick={() => navigate(OptionPath)}>
            <ListItemText sx={{ fontVariant: "body2" }} primary={OptionName} />
        </MenuItem>
    )
}
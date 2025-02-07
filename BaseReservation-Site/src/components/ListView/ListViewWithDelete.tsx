import { JSX, useState } from "react";
import DeleteIcon from '@mui/icons-material/Delete';
import { components } from "api/base-reservation/api";
import { Box, Checkbox, Grid2, IconButton, List, ListItem, ListItemText, Typography } from "@mui/material"

type SchemaType<T extends keyof components['schemas']> = components['schemas'][T];

interface ListViewProps<T extends keyof components['schemas']> {
    title: string,
    data: SchemaType<T>[],
    fieldForPrimaryText: keyof SchemaType<T> | ((item: SchemaType<T>) => string),
    onDelete: (ids: number[]) => void,
    enableDense?: boolean,
    enableSecondaryText?: boolean,
    fieldForSecondaryText?: keyof SchemaType<T> | ((item: SchemaType<T>) => string) | null
}

const removeItemFromSelection = (prev: number[], id: number) => {
    return prev.filter((itemId) => itemId !== id);
};

export const ListViewWithDelete = <T extends keyof components['schemas']>({
    title,
    data,
    fieldForPrimaryText,
    onDelete,
    enableDense = true,
    enableSecondaryText = false,
    fieldForSecondaryText = null
}: ListViewProps<T>) => {
    const [selectedItems, setSelectedItems] = useState<number[]>([]);
    const [selectAll, setSelectAll] = useState(false);

    const generate = (item: JSX.Element) => {
        return [item];
    };

    const handleDeleteClick = () => {
        onDelete(selectedItems);
        setSelectedItems([]);
        setSelectAll(false)
    };

    const handleDeleteById = (id: number) => {
        onDelete([id]);
    };

    const handleSelectAllChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const checked = event.target.checked;
        setSelectAll(checked);
        if (checked) {
            setSelectedItems(data.map((item) => (item as { id: number }).id));
        } else {
            setSelectedItems([]);
        }
    };

    const handleCheckboxChange = (id: number) => (event: React.ChangeEvent<HTMLInputElement>) => {
        const checked = event.target.checked;
        setSelectedItems((prev) => {
            if (checked) {
                return [...prev, id];
            } else {
                return removeItemFromSelection(prev, id);
            }
        });
    };

    const getPrimaryText = (item: SchemaType<T>) => {
        return typeof fieldForPrimaryText === 'function'
            ? fieldForPrimaryText(item)
            : String(item[fieldForPrimaryText]);
    };

    const getSecondaryText = (item: SchemaType<T>) => {
        if (enableSecondaryText && fieldForSecondaryText) {
            return typeof fieldForSecondaryText === 'function'
                ? fieldForSecondaryText(item)
                : String(item[fieldForSecondaryText]);
        }
        return null;
    };

    return (
        <Box sx={{ flexGrow: 1, pb: 2, width: '100%' }}>
            <Grid2 container spacing={2} sx={{ width: '100%' }}>
                <Grid2 columns={{ xs: 12 }} sx={{ width: '100%' }}>
                    <Box sx={{
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'space-between',
                        backgroundColor: '#5a7f7f',
                        color: 'common.white',
                        padding: 2,
                        borderRadius: '2px',
                        minHeight: '60px',
                        flexWrap: 'wrap',
                    }}>
                        <Checkbox
                            checked={selectAll}
                            onChange={handleSelectAllChange}
                            sx={{
                                color: 'common.white',
                                p: 0,
                                order: 1,
                            }}
                            inputProps={{ 'aria-label': 'select all' }}
                        />

                        <Typography variant="h6" component="div" sx={{
                            fontWeight: 'bold',
                            flexGrow: 1,
                            order: 2,
                            textAlign: 'center',
                            paddingBottom: { xs: 1, sm: 0 },
                        }}>
                            {title}
                        </Typography>

                        <IconButton
                            onClick={handleDeleteClick}
                            edge='end'
                            disabled={selectedItems.length === 0}
                            sx={{
                                '&:hover': { color: 'red' },
                                order: 3,
                            }}
                        >
                            <DeleteIcon fontSize="large" />
                        </IconButton>
                    </Box>


                    <List sx={{ py: 0 }} dense={enableDense}>
                        {data.length === 0 && (
                            <ListItem
                                sx={{
                                    py: '10px',
                                    minWidth: '100%',
                                    borderBottom: '1px solid black',
                                    borderTop: '1px solid black',
                                    backgroundColor: '#e5e5e5',
                                    display: 'flex',
                                    flexDirection: 'column',
                                    alignContent: 'center'
                                }}
                            >
                                <Typography sx={{ fontWeight: 'bold' }}>
                                    Sin registros
                                </Typography>
                            </ListItem>
                        )}

                        {data?.map((item, index) => {
                            const id = (item as { id: number }).id;
                            return (
                                generate(
                                    <ListItem
                                        key={`${id}-${index}`}
                                        sx={{
                                            px: 1,
                                            py: '1rem',
                                            minWidth: '100%',
                                            borderBottom: '1px solid black',
                                            borderTop: '1px solid black',
                                            backgroundColor: '#e5e5e5',
                                            justifyContent: 'space-between'
                                        }}
                                        secondaryAction={
                                            <IconButton edge="end" aria-label="delete" onClick={() => handleDeleteById(id)}
                                                sx={{
                                                    '&:hover': {
                                                        color: 'red'
                                                    },
                                                }}
                                            >
                                                <DeleteIcon fontSize="large" />
                                            </IconButton>
                                        }
                                    >
                                        <Checkbox
                                            checked={selectedItems.includes(id)}
                                            onChange={handleCheckboxChange(id)}
                                            color="primary"
                                            sx={{
                                                order: 0,
                                                marginRight: '10px'
                                            }}
                                        />
                                        <ListItemText
                                            primary={String(getPrimaryText(item))}
                                            secondary={enableSecondaryText && fieldForSecondaryText ? getSecondaryText(item) : null}
                                        />
                                    </ListItem>
                                )
                            )
                        })}
                    </List>
                </Grid2>
            </Grid2>
        </Box>
    )
}
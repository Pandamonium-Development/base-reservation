import { isNil } from "lodash";
import { useState } from "react";
import { isPresent } from 'utils/util';
import { UseLayout } from "hooks/UseLayout";
import { Page } from "components/Shared/Page";
import { useNavigate } from "react-router-dom";
import { useSnackbar } from "stores/useSnackbar";
import { yupResolver } from "@hookform/resolvers/yup";
import { TaxRequest } from "types/api-basereservation";
import { FormProvider, useForm } from "react-hook-form";
import { PageHeader } from "components/Shared/PageHeader";
import { TaxDefaultValues, TaxSchema } from "./TaxSchema";
import { FormButtons } from "components/Shared/FormButtons";
import { UsePutTax } from "hooks/api-basereservation/tax/UsePutTax";
import { Alert, Box, Button, Stack, TextField } from "@mui/material";
import { UsePostTax } from "hooks/api-basereservation/tax/UsePostTax";
import { FormFieldErrorMessage } from "components/FormFieldErrorMessage";
import { TaxDeleteModalConfirmation } from './TaxDeleteModalConfirmation';

export const TaxNewEdit = ({ taxData }: { taxData: TaxRequest | undefined }) => {
    const navigate = useNavigate();
    const { isMobile } = UseLayout();
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const [loading, setLoading] = useState(false);
    const formTitle = isNil(taxData) ? 'Crear nuevo impuesto' : `Editar impuesto número ${taxData.id}`;
    const isExisting = !isNil(taxData);

    const [openModalConfirmation, setOpenModalConfirmation] = useState(false);

    const formMethods = useForm({
        resolver: yupResolver(TaxSchema),
        defaultValues: isNil(taxData) ? {
            id: TaxDefaultValues.id,
            name: TaxDefaultValues.name,
            rate: TaxDefaultValues.rate,
        } : {
            id: Number(taxData.id),
            name: String(taxData.name),
            rate: Number(taxData.rate),
        }
    });

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = formMethods;

    const { mutate: postTax } = UsePostTax({
        onSuccess() {
            setSnackbarMessage('Impuesto creado correctamente');
            navigate('/General/Impuesto');
        },
        onError(data) {
            setSnackbarMessage(`${data.message}`, 'error');
        },
        onSettled() {
            setLoading(false);
        }
    })

    const { mutate: putTax } = UsePutTax({
        onSuccess() {
            setSnackbarMessage('Impuesto actualizado correctamente');
            navigate('/General/Impuesto');
        },
        onError(data) {
            setSnackbarMessage(`${data.message}`, 'error');
        },
        onSettled() {
            setLoading(false);
        }
    })

    const createTaxWrapper = handleSubmit((data) => {
        const formattedData = {
            name: data.name,
            rate: data.rate,
        }
        if (!isExisting) {
            postTax({ ...formattedData });
            return;
        }

        putTax({
            id: data.id,
            ...formattedData,
        })
    });

    return (
        <Page
            header={
                <PageHeader
                    title={formTitle}
                    subtitle="Debe completar los campos requeridos antes de guardar la información"
                    backText="Impuestos"
                    backPath="/General/Impuesto"
                    actionButton={
                        <Button sx={{ display: `${isExisting ? 'block' : 'none'}` }} variant="contained" size="large" fullWidth onClick={() => setOpenModalConfirmation(true)}>
                            Eliminar
                        </Button>
                    }
                />
            }
        >
            <FormProvider {...formMethods}>
                <form onSubmit={createTaxWrapper} noValidate>
                    <Box pb={2}>
                        {Object.keys(errors).length > 0 && (
                            <Alert severity="error">Por favor corrija los errores para continuar</Alert>
                        )}
                    </Box>
                    <Stack spacing={4} maxWidth={isMobile ? '90vw' : '600px'}>
                        <Box>
                            <TextField
                                required
                                error={isPresent(errors.name)}
                                label="Nombre"
                                placeholder="Nombre de la sucursal"
                                fullWidth
                                {...register('name')}
                            />
                            {errors.name?.message && (
                                <FormFieldErrorMessage message={errors.name.message} />
                            )}
                        </Box>

                        <Box>
                            <TextField
                                required
                                error={isPresent(errors.rate)}
                                label="Tasa"
                                type="number"
                                placeholder="Porcentaje de la tasa"
                                fullWidth
                                {...register('rate')}
                            />
                            {errors.rate?.message && (
                                <FormFieldErrorMessage message={errors.rate.message} />
                            )}
                        </Box>

                        <FormButtons backPath="/General/Impuesto" loadingIndicator={loading} />
                    </Stack>
                </form>
            </FormProvider>

            <TaxDeleteModalConfirmation
                isModalOpen={openModalConfirmation}
                taxId={taxData?.id ?? 0}
                toggleIsOpen={() => setOpenModalConfirmation(!openModalConfirmation)}
                title={taxData?.name ?? ''}
            />
        </Page>
    );
}
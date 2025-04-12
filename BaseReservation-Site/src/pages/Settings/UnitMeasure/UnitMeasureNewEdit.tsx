import { isNil } from "lodash";
import { useState } from "react";
import { isPresent } from 'utils/util';
import { UseLayout } from "hooks/UseLayout";
import { Page } from "components/Shared/Page";
import { yupResolver } from "@hookform/resolvers/yup";
import { FormProvider, useForm } from "react-hook-form";
import { PageHeader } from "components/Shared/PageHeader";
import { FormButtons } from "components/Shared/FormButtons";
import { UnitMeasureRequest } from "types/api-basereservation";
import { UseMutationCallbacks } from "hooks/UseMutationCallbacks";
import { Alert, Box, Button, Stack, TextField } from "@mui/material";
import { FormFieldErrorMessage } from "components/FormFieldErrorMessage";
import { UnitMeasureDefaultValues, UnitMeasureSchema } from "./UnitMeasureSchema";
import { UnitMeasureDeleteModalConfirmation } from './UnitMeasureDeleteModalConfirmation';
import { UsePutUnitMeasure } from "hooks/api-basereservation/unitMeasure/UsePutUnitMeasure";
import { UsePostUnitMeasure } from "hooks/api-basereservation/unitMeasure/UsePostUnitMeasure";

export const UnitMeasureNewEdit = ({ unitMeasureData }: { unitMeasureData: UnitMeasureRequest | undefined }) => {
    const { isMobile } = UseLayout();

    const [loading, setLoading] = useState(false);
    const formTitle = isNil(unitMeasureData) ? 'Crear nuevo unidad de medida' : `Editar unidad de medida número ${unitMeasureData.id}`;
    const isExisting = !isNil(unitMeasureData);

    const [openModalConfirmation, setOpenModalConfirmation] = useState(false);

    const closeLoading = () => setLoading(false);

    const formMethods = useForm({
        resolver: yupResolver(UnitMeasureSchema),
        defaultValues: isNil(unitMeasureData) ? {
            id: UnitMeasureDefaultValues.id,
            name: UnitMeasureDefaultValues.name,
            symbol: UnitMeasureDefaultValues.symbol,
        } : {
            id: Number(unitMeasureData.id),
            name: String(unitMeasureData.name),
            symbol: String(unitMeasureData.symbol),
        }
    });

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = formMethods;

    const { mutate: postUnitMeasure } = UsePostUnitMeasure(UseMutationCallbacks('Unidad de medida creada correctamente', '/General/UnidadMedida', closeLoading));
    const { mutate: putUnitMeasure } = UsePutUnitMeasure(UseMutationCallbacks('Unidad de medida actualizada correctamente', '/General/UnidadMedida', closeLoading));

    const createUnitMeasureWrapper = handleSubmit((data) => {
        const formattedData = {
            name: data.name,
            symbol: data.symbol,
        }
        if (!isExisting) {
            postUnitMeasure({ ...formattedData });
            return;
        }

        putUnitMeasure({
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
                    backText="Unidades de medida"
                    backPath="/General/UnidadMedida"
                    actionButton={
                        <Button sx={{ display: `${isExisting ? 'block' : 'none'}` }} variant="contained" size="large" fullWidth onClick={() => setOpenModalConfirmation(true)}>
                            Eliminar
                        </Button>
                    }
                />
            }
        >
            <FormProvider {...formMethods}>
                <form onSubmit={createUnitMeasureWrapper} noValidate>
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
                                error={isPresent(errors.symbol)}
                                label="Símbolo"
                                placeholder="Símbolo de la unidad de medida"
                                fullWidth
                                {...register('symbol')}
                            />
                            {errors.symbol?.message && (
                                <FormFieldErrorMessage message={errors.symbol.message} />
                            )}
                        </Box>

                        <FormButtons backPath="/General/Impuesto" loadingIndicator={loading} />
                    </Stack>
                </form>
            </FormProvider>

            <UnitMeasureDeleteModalConfirmation
                isModalOpen={openModalConfirmation}
                unitMeasureId={unitMeasureData?.id ?? 0}
                toggleIsOpen={() => setOpenModalConfirmation(!openModalConfirmation)}
                title={unitMeasureData?.name ?? ''}
            />
        </Page>
    );
}
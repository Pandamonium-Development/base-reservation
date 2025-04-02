import { isEmpty, isNil } from "lodash";
import { useLayout } from "hooks/useLayout";
import { Page } from "components/Shared/Page";
import { useNavigate } from "react-router-dom";
import { useSnackbar } from "stores/useSnackbar";
import { useEffect, useRef, useState } from "react";
import { yupResolver } from '@hookform/resolvers/yup';
import { PageHeader } from "components/Shared/PageHeader";
import { FormButtons } from "components/Shared/FormButtons";
import { CantonSelect } from "components/Directions/CantonSelect";
import { BranchDefaultValues, BranchSchema } from "./BranchSchema";
import { Controller, FormProvider, useForm } from "react-hook-form";
import { Alert, Box, Button, Stack, TextField } from "@mui/material";
import { DistrictSelect } from "components/Directions/DistrictSelect";
import { ProvinceSelect } from "components/Directions/ProvinceSelect";
import { applyPhoneMask, isPresent, removePhoneMask } from "utils/util";
import { FormFieldErrorMessage } from "components/FormFieldErrorMessage";
import { usePutBranch } from "hooks/api-basereservation/branch/usePutBranch";
import { usePostBranch } from "hooks/api-basereservation/branch/usePostBranch";
import { BaseReservationErrorDetails, Branch } from "types/api-basereservation";
import { BranchDeleteModalConfirmation } from "./BranchDeleteModalConfirmation";

export const BranchNewEdit = ({ branchData }: { branchData: Branch | undefined | null }) => {
    const navigate = useNavigate();
    const { isMobile } = useLayout();
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const [loading, setLoading] = useState(false);
    const [hasLoaded, setHasLoaded] = useState(false);
    const [province, setProvince] = useState(branchData?.district?.canton?.provinceId ?? 0);
    const [canton, setCanton] = useState(branchData?.district?.cantonId ?? 0);
    const [district, setDistrict] = useState(branchData?.districtId ?? 0);

    const initialProvinceRef = useRef(branchData?.district?.canton?.provinceId ?? 0);
    const initialCantonRef = useRef(branchData?.district?.cantonId ?? 0);

    const formTitle = isNil(branchData) ? 'Crear nueva sucursal' : `Editar sucursal número ${branchData.id}`;
    const isExisting = !isNil(branchData);

    const [openModalConfirmation, setOpenModalConfirmation] = useState(false);

    const formMethods = useForm({
        resolver: yupResolver(BranchSchema),
        defaultValues: isNil(branchData) ? BranchDefaultValues : {
            id: Number(branchData.id),
            name: String(branchData.name),
            description: String(branchData.description),
            telephone: applyPhoneMask(String(branchData.telephone)),
            email: String(branchData.email),
            provinceId: Number(branchData.district?.canton?.provinceId),
            cantonId: Number(branchData.district?.cantonId),
            districtId: Number(branchData.districtId),
            address: isNil(branchData.address) ? '' : branchData.address,
        }
    });

    const {
        control,
        register,
        handleSubmit,
        formState: { errors },
    } = formMethods;

    const { mutate: postBranch } = usePostBranch({
        onSuccess() {
            setSnackbarMessage('Sucursal creada correctamente');
            navigate('/Sucursal');
        },
        onError(data: BaseReservationErrorDetails) {
            setSnackbarMessage(`${data.message}`, 'error');
        },
        onSettled() {
            setLoading(false);
        }
    })

    const { mutate: putBranch } = usePutBranch({
        onSuccess() {
            setSnackbarMessage('Sucursal actualizada correctamente');
            navigate('/Sucursal');
        },
        onError(data: BaseReservationErrorDetails) {
            setSnackbarMessage(`${data.message}`, 'error');
        },
        onSettled() {
            setLoading(false);
        }
    })

    useEffect(() => {
        if (branchData) {
            setHasLoaded(true);
        }
    }, [branchData]);

    useEffect(() => {
        if (hasLoaded && province !== initialProvinceRef.current) {
            setCanton(0);
            setDistrict(0);
        }
    }, [province, hasLoaded]);

    useEffect(() => {
        if (hasLoaded && canton !== initialCantonRef.current) {
            setDistrict(0);
        }
    }, [canton, hasLoaded]);

    const createBranchWrapper = handleSubmit((data) => {
        setLoading(true);
        const formatedData = {
            name: data.name,
            description: data.description,
            telephone: removePhoneMask(data.telephone),
            email: data.email,
            districtId: data.districtId,
            address: isEmpty(data.address) ? null : data.address,
        }
        if (!isExisting) {
            postBranch({ ...formatedData });
            return;
        }

        putBranch({
            id: data.id,
            ...formatedData
        })
    });

    return (
        <Page
            header={
                <PageHeader
                    title={formTitle}
                    subtitle="Debe completar los campos requeridos antes de guardar la información"
                    backText="Sucursales"
                    backPath="/Sucursal"
                    actionButton={
                        <Button sx={{ display: `${isExisting ? 'block' : 'none'}` }} variant="contained" size="large" fullWidth onClick={() => setOpenModalConfirmation(true)}>
                            Eliminar
                        </Button>
                    }
                />
            }
        >
            <FormProvider {...formMethods}>
                <form onSubmit={createBranchWrapper} noValidate>
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
                                error={isPresent(errors.description)}
                                label="Descripción"
                                multiline
                                placeholder="Descripción de la sucursal"
                                fullWidth
                                {...register('description')}
                            />
                            {errors.description?.message && (
                                <FormFieldErrorMessage message={errors.description.message} />
                            )}
                        </Box>

                        <Box>
                            <Controller
                                name="telephone"
                                control={control}
                                defaultValue=""
                                render={({ field: { onChange, onBlur, value } }) => (
                                    <TextField
                                        required
                                        error={isPresent(errors.telephone)}
                                        label="Teléfono"
                                        placeholder="Teléfono de la sucursal"
                                        fullWidth
                                        onChange={(e) => {
                                            let numericValue = e.target.value.replace(/\D/g, '');
                                            if (numericValue.length > 8) {
                                                numericValue = numericValue.slice(0, 8);
                                            }
                                            onChange(applyPhoneMask(numericValue));
                                        }}
                                        onBlur={onBlur}
                                        value={value}
                                    />
                                )}
                            />
                            {errors.telephone?.message && (
                                <FormFieldErrorMessage message={errors.telephone.message} />
                            )}
                        </Box>

                        <Box>
                            <TextField
                                required
                                error={isPresent(errors.email)}
                                label="Correo electrónico"
                                placeholder="Correo electrónico de la sucursal"
                                fullWidth
                                {...register('email')}
                            />
                            {errors.email?.message && (
                                <FormFieldErrorMessage message={errors.email.message} />
                            )}
                        </Box>

                        <ProvinceSelect selectedProvince={province} onProvinceChange={setProvince} />
                        <CantonSelect selectedProvince={province} selectedCanton={canton} onCantonChange={setCanton} />
                        <Box>
                            <Controller
                                name="districtId"
                                control={control}
                                defaultValue={district}
                                render={({ field }) => (
                                    <DistrictSelect
                                        selectedProvince={province}
                                        selectedCanton={canton}
                                        selectedDistrict={district}
                                        onDistrictChange={(newDistrict) => {
                                            field.onChange(newDistrict);
                                            setDistrict(newDistrict);
                                        }}
                                        errorForm={!!errors.districtId}
                                    />
                                )}
                            />
                            {errors.districtId?.message && (
                                <FormFieldErrorMessage message={errors.districtId.message} />
                            )}
                        </Box>

                        <Box>
                            <TextField
                                error={isPresent(errors.address)}
                                label="Dirección exacta"
                                multiline
                                placeholder="Dirección exacta de la sucursal"
                                fullWidth
                                {...register('address')}
                            />
                            {errors.address?.message && (
                                <FormFieldErrorMessage message={errors.address.message} />
                            )}
                        </Box>

                        <FormButtons backPath="/Sucursal" loadingIndicator={loading} />
                    </Stack>
                </form>
            </FormProvider>

            <BranchDeleteModalConfirmation
                isModalOpen={openModalConfirmation}
                toggleIsOpen={() => setOpenModalConfirmation(!openModalConfirmation)}
                branchId={branchData?.id ?? 0}
                title={branchData?.name ?? ""}
            />
        </Page>
    );
}
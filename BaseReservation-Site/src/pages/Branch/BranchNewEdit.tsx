import { isEmpty, isNil } from "lodash";
import { useEffect, useState } from "react";
import { useLayout } from "hooks/useLayout";
import { Page } from "components/Shared/Page";
import { useSnackbar } from "stores/useSnackbar";
import { yupResolver } from '@hookform/resolvers/yup';
import { PageHeader } from "components/Shared/PageHeader";
import { CantonSelect } from "components/Directions/CantonSelect";
import { useNavigate, Link as RouterLink } from "react-router-dom";
import { BranchDefaultValues, BranchSchema } from "./BranchSchema";
import { Controller, FormProvider, useForm } from "react-hook-form";
import { DistrictSelect } from "components/Directions/DistrictSelect";
import { ProvinceSelect } from "components/Directions/ProvinceSelect";
import { applyPhoneMask, isPresent, removePhoneMask } from "utils/util";
import { usePostBranch } from "hooks/api-basereservation/usePostBranch";
import { FormFieldErrorMessage } from "components/FormFieldErrorMessage";
import { BaseReservationErrorDetails, Branch } from "types/api-basereservation";
import { Alert, Box, Button, FormControlLabel, Stack, Switch, TextField } from "@mui/material";

export const BranchNewEdit = ({ branchData }: { branchData: Branch | undefined | null }) => {
    const navigate = useNavigate();
    const { isMobile } = useLayout();
    const setMessage = useSnackbar((state) => state.setMessage);
    const [province, setProvince] = useState(isNil(branchData) ? 0 : Number(branchData.district?.canton?.provinceId));
    const [canton, setCanton] = useState(isNil(branchData) ? 0 : Number(branchData.district?.cantonId));
    const [district, setDistrict] = useState(isNil(branchData) ? 0 : Number(branchData.districtId));

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
            active: Boolean(branchData.active)
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
            setMessage('Sucursal creada correctamente');
            navigate('/Sucursal');
        },
        onError(data: BaseReservationErrorDetails) {
            setMessage(`${data.message}`, 'error');
        }
    })

    useEffect(() => {
        setCanton(canton);
        setDistrict(district);
    }, [province, canton, district]);

    useEffect(() => {
        setDistrict(district);
    }, [canton, district]);

    const createBranchWrapper = handleSubmit((data) => {
        postBranch({
            name: data.name,
            description: data.description,
            telephone: removePhoneMask(data.telephone),
            email: data.email,
            districtId: data.districtId,
            address: isEmpty(data.address) ? null : data.address,
            active: data.active
        });
    });

    return (
        <Page
            header={
                <PageHeader
                    title="Crear nueva sucursal"
                    subtitle="Debe completar los campos requeridos antes de guardar la información"
                    backText="Sucursales"
                    backPath="/Sucursal"
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
                                        error={!!errors.districtId}
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
                        <Box display="flex" justifyContent="center" alignItems="center" width="100%">
                            <Controller
                                name="active"
                                control={control}
                                defaultValue={true}
                                render={({ field }) => (
                                    <FormControlLabel
                                        control={
                                            <Switch
                                                {...field}
                                                checked={field.value}
                                                onChange={(e) => field.onChange(e.target.checked)}
                                            />
                                        }
                                        label="Activo"
                                        labelPlacement="start"
                                        disabled
                                    />
                                )}
                            />
                            {errors.active?.message && (
                                <FormFieldErrorMessage message={errors.active.message} />
                            )}
                        </Box>
                        {isMobile && (
                            <Box display='flex' justifyContent='space-between' maxWidth='90vw' gap='8px'>
                                <Box flex={1} px={1} pr={2} sx={{ pl: 0 }}>
                                    <RouterLink to="/Sucursal">
                                        <Button variant="outlined" fullWidth>Cancelar</Button>
                                    </RouterLink>
                                </Box>
                                <Box flex={1} px={1} pl={2} sx={{ pr: 0 }}>
                                    <Button type="submit" variant="contained" fullWidth>
                                        Guardar
                                    </Button>
                                </Box>
                            </Box>
                        )}
                        {!isMobile && (
                            <Stack direction='row' spacing={2} justifyContent='flex-end'>
                                <RouterLink to="/Sucursal">
                                    <Button variant="outlined">Cancelar</Button>
                                </RouterLink>
                                <Button type="submit" variant="contained">
                                    Guardar
                                </Button>
                            </Stack>
                        )}
                    </Stack>
                </form>
            </FormProvider>
        </Page>
    );
}
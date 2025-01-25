import { useEffect, useState } from "react";
import { useLayout } from "hooks/useLayout";
import { Page } from "components/Shared/Page";
import { useSnackbar } from "stores/useSnackbar";
import { useMutation } from "@tanstack/react-query";
import { yupResolver } from '@hookform/resolvers/yup';
import { PageHeader } from "components/Shared/PageHeader";
import { BranchDefaultValues, BranchSchema, type BranchForm } from "./BranchSchema";
import { useTypedApiClientBS } from "hooks/useTypedApiClientBS";
import { CantonSelect } from "components/Directions/CantonSelect";
import { useNavigate, Link as RouterLink } from "react-router-dom";
import { Controller, FormProvider, useForm } from "react-hook-form";
import { Alert, Box, Button, FormControlLabel, Stack, Switch, TextField } from "@mui/material";
import { DistrictSelect } from "components/Directions/DistrictSelect";
import { ProvinceSelect } from "components/Directions/ProvinceSelect";
import { applyPhoneMask, isPresent, removePhoneMask } from "utils/util";
import { FormFieldErrorMessage } from "components/FormFieldErrorMessage";

export const BranchNewEdit = () => {
    const navigate = useNavigate();
    const { isMobile } = useLayout();
    const setMessage = useSnackbar((state) => state.setMessage);
    const [province, setProvince] = useState(0);
    const [canton, setCanton] = useState(0);
    const [district, setDistrict] = useState(0);

    const formMethods = useForm({
        resolver: yupResolver(BranchSchema),
        defaultValues: BranchDefaultValues
    });

    const {
        control,
        register,
        handleSubmit,
        formState: { errors },
    } = formMethods;

    const postBranch = useTypedApiClientBS({
        path: '/api/Branch',
        method: 'post'
    })

    const createBranch = useMutation({
        mutationKey: ['Branches'],
        mutationFn: async (data: BranchForm) => {
            return await postBranch({
                name: data.name,
                description: data.description,
                telephone: removePhoneMask(data.telephone),
                email: data.email,
                districtId: data.districtId,
                address: data.address,
                active: data.active
            })
        },
        onSuccess: () => {
            navigate('/Sucursal');
            setMessage('Sucursal creada correctamente', 'success');
        },
        onError: (error) => {
            setMessage(error.message, 'error');
        }
    })

    useEffect(() => {
        setCanton(0);
        setDistrict(0);
    }, [province]);

    useEffect(() => {
        setDistrict(0);
    }, [canton]);

    const createBranchWrapper = handleSubmit((data) => {
        createBranch.mutate(data);
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